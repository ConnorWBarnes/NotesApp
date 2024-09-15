/**
 * Appends the given path to the given base URL.
 * @param baseUrl The base url to append.
 * @param path The path to add to the url.
 * @returns The transformed URL with the given path added.
 */
export function appendPathToUrl(baseUrl: string, path: string): string {
  return `${baseUrl}${baseUrl.trim().endsWith('/') ? '' : '/'}${path}`;
}

/**
 * Adds the given query parameter to the given base URL.
 * @param baseUrl The base url to append.
 * @param key The query parameter key.
 * @param value The query parameter value.
 * @returns The transformed URL with the given query parameter added.
 */
export function addQueryToUrl(baseUrl: string, key: string, value: string): string {
  return `${baseUrl}${baseUrl.trim().includes('?') ? '&' : '?'}${key}=${encodeURIComponent(value)}`;
}

/**
 * Labels and logs a message.
 * @param source The source of the message.
 * @param message The message to log.
 */
export function log(source: string, message: string): void {
  const labeledMessage = `${source}: ${message}`;
  console.log(labeledMessage)
}

/**
 * Handles a failed HTTP operation and allows the app to continue.
 * @param source The source of the error.
 * @param error The error that occurred.
 * @param operation The name of the operation that failed.
 * @param result Optional value to return as a Promise.
 * @returns A Promise of the given result.
 */
export async function handleErrorAsync<T>(source: string, error: any, operation = "operation", result?: T): Promise<T> {
  // TODO: Send the error to remote logging infrastructure
  console.error(error);

  // TODO: Improve error transformation for user consumption
  log(source, `${operation} failed: ${error.message}`);

  // Return an empty result to allow the app to continue running
  return Promise.resolve(result as T);
}