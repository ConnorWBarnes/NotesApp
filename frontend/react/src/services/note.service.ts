import { addAuthTokenInjectionInterceptor, addAuthTokenRefreshInterceptor } from "@/utils/auth-utils";
import axios, { AxiosInstance } from "axios";
import { Note } from "@/types/note";
import { handleErrorAsync } from "@/utils/logging-utils";

const logSource = 'NoteService';

// TODO: Refactor into class that uses axios client
export class NoteService {
  protected readonly instance: AxiosInstance;

  public constructor(url: string) {
    this.instance = axios.create({
      baseURL: `${url}/notes`,
      timeout: 50000,
      timeoutErrorMessage: "Note request timed out",
    });
    addAuthTokenInjectionInterceptor(this.instance);
    addAuthTokenRefreshInterceptor(this.instance);
  }

  /**
   * GET: Gets all notes from the server.
   * @returns An Observable of all the notes retrieved.
   */
  public async getNotesAsync(): Promise<Note[]> {
    try {
      let response = await this.instance.get<Note[]>('/');
      return response.data;
    } catch (error) {
      return handleErrorAsync(logSource, error, 'getNotesAsync');
    }
  }

  /**
   * GET: Gets a note by ID. Will 404 if not found.
   * @param id The ID of the note to get.
   * @returns A Promise of the specified note.
   */
  public async getNoteAsync(id: string): Promise<Note> {
    try {
      let response = await this.instance.get<Note>(`/${id}`);
      return response.data;
    } catch (error) {
      return handleErrorAsync(logSource, error, 'getNoteAsync');
    }
  }

  /**
   * GET: Gets all archived notes from the server.
   * @returns A Promise of all the notes retrieved.
   */
  public async getArchivedNotesAsync(): Promise<Note[]> {
    try {
      let response = await this.instance.get<Note[]>('/archive');
      return response.data;
    } catch (error) {
      return handleErrorAsync(logSource, error, 'getArchivedNotesAsync');
    }
  }

  /**
   * POST: Creates and adds a new note to the server.
   * @param note The note to create and add to the server.
   * @returns A Promise of the ID of the newly created note.
   */
  public async createNoteAsync(note: Note): Promise<string> {
    try {
      let response = await this.instance.post<NoteCreatedResponse>('/', JSON.stringify(note));
      return response.data.id;
    } catch (error) {
      return handleErrorAsync(logSource, error, 'createNoteAsync');
    }
  }

  /**
   * PUT: Updates the note on the server.
   * @param note The updated note.
   * @returns A flag indicating the success of the operation.
   */
  public async updateNoteAsync(note: Note): Promise<boolean> {
    try {
      let response = await this.instance.put(`/${note.id}`, JSON.stringify(note));
      return response.status.toString().startsWith('2');
    } catch (error) {
      return handleErrorAsync(logSource, error, 'updateNoteAsync', false);
    }
  }

  /**
   * DELETE: Deletes a note from the server.
   * @param note The note or the ID of the note to delete.
   * @returns A flag indicating the success of the operation.
   */
  public async deleteNoteAsync(note: Note | string): Promise<boolean> {
    try {
      const id = typeof note === "string" ? note : note.id;
      let response = await this.instance.delete(`/${id}`);
      return response.status.toString().startsWith('2');
    } catch (error) {
      return handleErrorAsync(logSource, error, 'deleteNoteAsync', false);
    }
  }
}

interface NoteCreatedResponse {
  id: string;
}
