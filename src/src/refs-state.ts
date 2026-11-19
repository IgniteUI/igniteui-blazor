export const refValues: Map<string, any> = new Map<string, any>();
export const itemMaps: Map<string, Map<string, any>> = new Map<string, Map<string, any>>();

/**
 * The data items that a component holds past its data. A combo keeps its selection, and the
 * pending selection of its confirm mode, when its data is replaced or the items leave it.
 * Other components hold none.
 */
export function heldItems(element: any): any[] {
  if (element?.tagName !== 'IGC-COMBO') {
    return [];
  }
  const held: any[] = [];
  for (const item of element.selection ?? []) {
    held.push(item);
  }
  // The pending records of the confirm mode have no public getter. Read the field, not the
  // `pending` getter, which drops a pending selection as a side effect.
  const pending: Map<any, any> | undefined = element._selection?._pending;
  if (pending) {
    for (const item of pending.values()) {
      if (item !== undefined) {
        held.push(item);
      }
    }
  }
  return held;
}

/** The uuids of the items that {@link heldItems} returns. */
export function heldItemIds(element: any): string[] {
  const ids: string[] = [];
  for (const item of heldItems(element)) {
    if (item !== null && typeof item === 'object' && item.___id) {
      ids.push(item.___id);
    }
  }
  return ids;
}

function findHeldItem(id: string): any {
  if (typeof document === 'undefined') {
    return undefined;
  }
  const combos = document.querySelectorAll('igc-combo');
  for (let i = 0; i < combos.length; i++) {
    const element = combos[i];
    for (const item of heldItems(element)) {
      if (item !== null && typeof item === 'object' && item.___id === id) {
        return item;
      }
    }
  }
  return undefined;
}

/**
 * The item of a uuid ref: from the data refs, or else from the items that a component still holds
 * after they left its data. A uuid that neither knows stays unresolved.
 */
export function findItem(id: string): any {
  for (const map of itemMaps.values()) {
    if (map.has(id)) {
      return map.get(id);
    }
  }
  return findHeldItem(id);
}
