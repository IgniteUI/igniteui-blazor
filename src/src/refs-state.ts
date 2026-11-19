export const refValues: Map<string, any> = new Map<string, any>();
export const itemMaps: Map<string, Map<string, any>> = new Map<string, Map<string, any>>();

// Items of data refs that were cleaned up, held weakly by their uuid. A component can keep
// such items on purpose, as a combo keeps its selection when its data is replaced; a uuid ref
// to one of them still resolves while the component holds it. The rest are garbage collected.
const retiredItems: Map<string, WeakRef<any>> = new Map<string, WeakRef<any>>();
const retiredRegistry = new FinalizationRegistry<string>((id) => {
  const ref = retiredItems.get(id);
  if (ref && ref.deref() === undefined) {
    retiredItems.delete(id);
  }
});

export function retireItem(id: string, item: any): void {
  if (id && item !== null && typeof item === 'object') {
    retiredItems.set(id, new WeakRef(item));
    retiredRegistry.register(item, id);
  }
}

export function retireItemMap(map: Map<string, any>): void {
  map.forEach((item, id) => retireItem(id, item));
}

export function findItem(id: string): any {
  let found: any = undefined;
  for (const map of itemMaps.values()) {
    if (map.has(id)) {
      found = map.get(id);
      break;
    }
  }
  if (found === undefined) {
    found = retiredItems.get(id)?.deref();
  }
  return found;
}
