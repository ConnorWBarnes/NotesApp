import { Note } from "@/types/note";
import { handleErrorAsync, log } from "@/utils/logging-utils";
import { appendPathToUrl } from "@/utils/url-utils";

const notesUrl = 'http://localhost:3001/notes';

const httpHeaders = { 'Content-Type': 'application/json' };

const logSource = 'NoteService';

/**
 * GET: Gets all notes from the server.
 * @returns An Observable of all the notes retrieved.
 */
export async function getNotesAsync(): Promise<Note[]> {
  return await fetch(notesUrl, { cache: 'no-store', method: 'GET' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'getNotesAsync');
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
  const url = appendPathToUrl(notesUrl, id);
  return await fetch(url, { cache: 'no-store', method: 'GET' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'getNoteAsync');
      }
      return await response.json() as Note;
    });
}


/**
 * GET: Gets all archived notes from the server.
 * @returns A Promise of all the notes retrieved.
 */
export async function getArchivedNotesAsync(): Promise<Note[]> {
  const url = appendPathToUrl(notesUrl, 'archive');
  return await fetch(url, { cache: 'no-store', method: 'GET' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'getArchivedNotesAsync');
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
  return await fetch(notesUrl, { cache: 'no-store', method: 'POST', headers: httpHeaders, body: JSON.stringify(note) })
    .then(async response => {
      const responseString = await response.json();
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(responseString), 'createNoteAsync');
      }

      log(logSource, `createNoteAsync: Created note with ID = ${responseString}`);
      return responseString;
    })
}

/**
 * PUT: Updates the note on the server.
 * @param note The updated note.
 * @returns A flag indicating the success of the operation.
 */
export async function updateNoteAsync(note: Note): Promise<boolean> {
  const url = appendPathToUrl(notesUrl, note.id);
  return await fetch(url, { cache: 'no-store', method: 'PUT', headers: httpHeaders, body: JSON.stringify(note) })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'updateNoteAsync', response.ok)
      }

      log(logSource, `updateNoteAsync: Updated note with ID = ${note.id}`);
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
  const url = appendPathToUrl(notesUrl, id);
  return await fetch(url, { cache: 'no-store', method: 'DELETE' })
    .then(async response => {
      if (!response.ok) {
        return handleErrorAsync(logSource, new Error(await response.json()), 'deleteNoteAsync', response.ok);
      }

      log(logSource, `deleteNoteAsync: Deleted note with ID = ${id}`);
      return response.ok;
    })
}

