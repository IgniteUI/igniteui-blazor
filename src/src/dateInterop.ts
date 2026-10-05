const pad = (num: number, size: number): string => {
  let str = Math.abs(num).toString();
  while (str.length < size) {
    str = '0' + str;
  }
  return num < 0 ? '-' + str : str;
};

/**
 * Formats a Date using its local (wall clock) components as an ISO 8601 string
 * without a timezone designator, e.g. "2024-01-15T10:30:00.000".
 *
 * Date.prototype.toJSON/toISOString always converts to UTC, which shifts the
 * wall clock value when it round trips back into .NET. Serializing the local
 * components keeps the value the user sees in the browser identical to the
 * reading parsed on the .NET side.
 */
export function toLocalISOString(value: Date): string {
  return (
    pad(value.getFullYear(), 4) +
    '-' +
    pad(value.getMonth() + 1, 2) +
    '-' +
    pad(value.getDate(), 2) +
    'T' +
    pad(value.getHours(), 2) +
    ':' +
    pad(value.getMinutes(), 2) +
    ':' +
    pad(value.getSeconds(), 2) +
    '.' +
    pad(value.getMilliseconds(), 3)
  );
}

/**
 * Formats a Date as its local reading plus the browser's real UTC offset, e.g.
 * "2024-01-15T10:30:00.000+03:00".
 *
 * This is the shape every date takes on its way back to .NET. The reading lets a
 * wall clock value round trip untouched, while the offset lets an instant be
 * reconstructed exactly, so the original DateTimeKind can be restored.
 */
export function toLocalISOStringWithOffset(value: Date): string {
  // getTimezoneOffset is minutes to add to local time to reach UTC, so the sign
  // is the reverse of the ISO designator.
  const offsetMinutes = -value.getTimezoneOffset();
  const sign = offsetMinutes < 0 ? '-' : '+';
  const absolute = Math.abs(offsetMinutes);

  return toLocalISOString(value) + sign + pad(Math.floor(absolute / 60), 2) + ':' + pad(absolute % 60, 2);
}

/**
 * Formats a Date as a bare calendar date, e.g. "2024-01-15", from its local
 * components. A date-only value carries no instant, so it must never be routed
 * through a UTC conversion.
 */
export function toLocalDateString(value: Date): string {
  return pad(value.getFullYear(), 4) + '-' + pad(value.getMonth() + 1, 2) + '-' + pad(value.getDate(), 2);
}

/**
 * Parses a wire date payload into a Date.
 *
 * A bare "yyyy-MM-dd" is built through the local Date constructor on purpose:
 * `new Date("2024-01-15")` is parsed as UTC per the ECMAScript spec and lands on
 * the previous day for every browser west of Greenwich. Anything carrying a time
 * is handed to the regular parser, which honours the designator when present and
 * treats a suffix-free reading as local.
 */
export function parseWireDate(value: string): Date {
  const dateOnly = /^(\d{4})-(\d{2})-(\d{2})$/.exec(value);
  if (dateOnly) {
    return new Date(+dateOnly[1], +dateOnly[2] - 1, +dateOnly[3]);
  }
  return new Date(value);
}
