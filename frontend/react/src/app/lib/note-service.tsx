import Error from "next/error";
import { Note } from "@/app/lib/note";

const notesUrl = 'http://localhost:3001/notes';

const httpHeaders = { 'Content-Type': 'application/json' };

/**
 * GET: Gets all notes from the server.
 * @returns An Observable of all the notes retrieved.
 */
export async function getNotesAsync(): Promise<Note[]> {
  return await fetch(notesUrl)
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(new Error(await response.json()), 'getNotesAsync');
      }
      return await response.json() as Note[];
    });
}

/**
 * GET: Gets a note by ID. Will 404 if not found.
 * @param id The ID of the note to get.
 * @returns A Promise of the specified note.
 */
export async function getNoteAsync(id: string): Promise<Note> {
  const url = appendToUrl(id);
  return await fetch(url, { method: 'GET' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(new Error(await response.json()), 'getNoteAsync');
      }
      return await response.json() as Note;
    });
}


/**
 * GET: Gets all archived notes from the server.
 * @returns A Promise of all the notes retrieved.
 */
export async function getArchivedNotesAsync(): Promise<Note[]> {
  const url = appendToUrl('archive');
  return await fetch(url, { method: 'GET' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(new Error(await response.json()), 'getArchivedNotesAsync');
      }
      return await response.json() as Note[];
    });
}

/**
 * POST: Creates and adds a new note to the server.
 * @param note The note to create and add to the server.
 * @returns A Promise of the ID of the newly created note.
 */
export async function createNoteAsync(note: Note): Promise<string> {
  return await fetch(notesUrl, { method: 'POST', headers: httpHeaders, body: JSON.stringify(note) })
    .then(async response => {
      const responseString = await response.json();
      if (!response.ok) {
        return handleErrorAsync(new Error(responseString), 'createNoteAsync');
      }

      log(`createNoteAsync: Created note with ID = ${responseString}`);
      return responseString;
    })
}

/**
 * PUT: Updates the note on the server.
 * @param note The updated note.
 * @returns A flag indicating the success of the operation.
 */
export async function updateNoteAsync(note: Note): Promise<boolean> {
  const url = appendToUrl(note.id);
  return await fetch(url, { method: 'PUT', headers: httpHeaders, body: JSON.stringify(note) })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(new Error(await response.json()), 'updateNoteAsync', response.ok)
      }

      log(`updateNoteAsync: Updated note with ID = ${note.id}`);
      return response.ok;
    })
}

/**
 * DELETE: Deletes a note from the server.
 * @param note The note or the ID of the note to delete.
 * @returns A flag indicating the success of the operation.
 */
export async function deleteNoteAsync(note: Note | string): Promise<boolean> {
  const id = typeof note === "string" ? note : note.id;
  const url = appendToUrl(id);
  return await fetch(url, { method: 'DELETE' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(new Error(await response.json()), 'deleteNoteAsync', response.ok);
      }

      log(`deleteNoteAsync: Deleted note with ID = ${id}`);
      return response.ok;
    })
}

/**
 * Appends the given string to the base notes URL.
 * @param str The string to add to the url.
 * @returns The transformed URL with the given str added.
 */
function appendToUrl(str: string): string {
  return `${notesUrl}/${str}`;
}

/**
 * Labels and logs a message.
 * @param message The message to log.
 */
function log(message: string): void {
  const labeledMessage = `NoteService: ${message}`;
  console.log(labeledMessage)
}

/**
 * Handles a failed HTTP operation and allows the app to continue.
 * @param error The error that occurred.
 * @param operation The name of the operation that failed.
 * @param result Optional value to return as a Promise.
 * @returns A Promise of the given result.
 */
function handleErrorAsync<T>(error: any, operation = "operation", result?: T): Promise<T> {
  // TODO: Send the error to remote logging infrastructure
  console.error(error);

  // TODO: Improve error transformation for user consumption
  log(`${operation} failed: ${error.message}`);

  // Return an empty result to allow the app to continue running
  return Promise.resolve(result as T);
}