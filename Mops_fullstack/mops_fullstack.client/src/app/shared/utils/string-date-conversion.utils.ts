export function stringToDate(input: string): Date | null {
  let parts = input.split('/', 3);
  if (parts.length != 3) {
    return null;
  }

  return new Date(Number(parts[0]), Number(parts[1]) - 1, Number(parts[2]));
}

export function dateToString(input: Date): string {
  return input.getFullYear().toString() + "/" + (input.getMonth() + 1) + "/" + input.getDate();
}

