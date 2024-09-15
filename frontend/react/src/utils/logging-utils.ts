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