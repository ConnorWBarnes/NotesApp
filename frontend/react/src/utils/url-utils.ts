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
