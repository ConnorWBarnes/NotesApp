import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';

import { Observable, firstValueFrom, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Note } from './note';

/**
 * Service for managing notes.
 */
@Injectable({
  providedIn: 'root'
})
export class NoteService {
  notesUrl = 'http://localhost:3001/notes';

  httpHeaders = new HttpHeaders(
    {
      'Content-Type': 'application/json',
    }
  );

  constructor(private http: HttpClient) { }
  
  /**
   * GET: Gets all notes from the server.
   * @returns An Observable of all the notes retrieved.
   */
  getNotes$(): Observable<Note[]> {
    return this.http.get<Note[]>(this.notesUrl).pipe(
        tap(notes => this.log(`getNotes$: Retrieved ${notes.length} notes`)),
        catchError(this.handleError<Note[]>('getNotes$', []))
      );
  }

  /**
   * GET: Gets all notes from the server.
   * @returns A Promise of all the notes retrieved.
   */
  async getNotesAsync(): Promise<Note[]> {
    return await firstValueFrom(this.http.get<Note[]>(this.notesUrl).pipe(
      tap(notes => this.log(`getNotesAsync: Retrieved ${notes.length} notes`)),
      catchError(this.handleError<Note[]>('getNotesAsync', []))
    ));
  }

  /**
   * GET: Gets all archived notes from the server.
   * @returns An Observable of all the notes retrieved.
   */
  getArchivedNotes$(): Observable<Note[]> {
    const url = this.appendToUrl('archive');
    return this.http.get<Note[]>(url).pipe(
        tap(notes => this.log(`getArchivedNotes$: Retrieved ${notes.length} archived notes`)),
        catchError(this.handleError<Note[]>('getArchivedNotes$', []))
      );
  }

  /**
   * GET: Gets all archived notes from the server.
   * @returns A Promise of all the notes retrieved.
   */
  async getArchivedNotesAsync(): Promise<Note[]> {
    const url = this.appendToUrl('archive');
    return await firstValueFrom(this.http.get<Note[]>(url).pipe(
      tap(notes => this.log(`getArchivedNotesAsync: Retrieved ${notes.length} archived notes`)),
      catchError(this.handleError<Note[]>('getArchivedNotesAsync', []))
    ));
  }

  /**
   * GET: Gets a note by ID. Will 404 if not found.
   * @param id The ID of the note to get.
   * @returns An Observable of the specified note.
   */
  getNote$(id: string): Observable<Note> {
    const url = this.appendToUrl(id);
    return this.http.get<Note>(url).pipe(
      tap(_ => this.log(`getNote$: Retrieved note ID = ${id}`)),
      catchError(this.handleError<Note>(`getNote$ ID = ${id}`))
    );
  }

  /**
   * GET: Gets a note by ID. Will 404 if not found.
   * @param id The ID of the note to get.
   * @returns A Promise of the specified note.
   */
  async getNoteAsync(id: string): Promise<Note> {
    const url = this.appendToUrl(id);
    return await firstValueFrom(this.http.get<Note>(url).pipe(
      tap(_ => this.log(`getNoteAsync: Retrieved note ID = ${id}`)),
      catchError(this.handleError<Note>(`getNoteAsync ID = ${id}`))
    ));
  }

  /**
   * GET: Gets a note by ID. Returns 'undefined' when ID not found.
   * @param id The ID of the Note to get.
   * @returns An Observable of the specified note (or 'undefined' if not found).
   */
  getNoteNo404$<Data>(id: string): Observable<Note> {
    const url = `${this.notesUrl}/?id=${id}`;
    return this.http.get<Note[]>(url).pipe(
      map(notes => notes[0]), // returns a {0|1} element array
      tap(h => {
        const outcome = h ? 'retrieved' : 'did not find';
        this.log(`getNote: ${outcome} note id=${id}`);
      }),
      catchError(this.handleError<Note>(`getNoteNo404$: Retrieved note ID = ${id}`))
    );
  }

  /**
   * POST: Creates and adds a new note to the server.
   * @param note The note to create and add to the server.
   * @returns An Observable of the ID of the newly created note.
   */
  createNote$(note: Note): Observable<string> {
    return this.http.post<string>(this.notesUrl, note, { headers: this.httpHeaders }).pipe(
      tap((newNoteId: string) => this.log(`createNote$: Created note with ID = ${newNoteId}`)),
      catchError(this.handleError<string>('createNote$'))
    );
  }

  /**
   * POST: Creates and adds a new note to the server.
   * @param note The note to create and add to the server.
   * @returns A Promise of the ID of the newly created note.
   */
  async createNoteAsync(note: Note): Promise<string> {
    return await firstValueFrom(this.http.post<string>(this.notesUrl, note, { headers: this.httpHeaders }).pipe(
      tap((newNoteId: string) => this.log(`createNoteAsync: Created note with ID = ${newNoteId}`)),
      catchError(this.handleError<string>('createNoteAsync'))
    ));
  }

  /**
   * PUT: Updates the note on the server.
   * @param note The updated note.
   */
  updateNote$(note: Note): Observable<any> {
    const url = `${this.notesUrl}/${note.id}`;
    return this.http.put(url, note, { headers: this.httpHeaders }).pipe(
      tap(_ => this.log(`updateNote$: Updated note with ID = ${note.id}`)),
      catchError(this.handleError<any>('updateNote$'))
    );
  }
  
  /**
   * PUT: Updates the note on the server.
   * @param note The updated note.
   * @returns A flag indicating the success of the opertation.
   */
  async updateNoteAsync(note: Note): Promise<boolean> {
    // Send the request to update the note
    const url = this.appendToUrl(note.id);
    const response = await firstValueFrom(this.http.put(url, note, { headers: this.httpHeaders, observe: 'response' }));
    
    // Handle the response
    if (response instanceof HttpErrorResponse) {
      this.handleErrorAsync(new Error(response.message), 'updateNoteAsync');
    } else {
      this.log(`updateNoteAsync: Updated note with ID = ${note.id}`);
    }

    // Return the result
    return response.ok;
  }

  /**
   * DELETE: Deletes a note from the server.
   * @param note The note or the ID of the note to delete.
   */
  deleteNote$(note: Note | string): Observable<any> {
    const id = typeof note === "string" ? note : note.id;
    const url = this.appendToUrl(id);

    return this.http.delete(url, { headers: this.httpHeaders }).pipe(
      tap(_ => this.log(`deleteNote$: Deleted note ID = ${id}`)),
      catchError(this.handleError<any>('deleteNote$'))
    );
  }

  /**
   * DELETE: Deletes a note from the server.
   * @param note The note or the ID of the note to delete.
   * @returns A flag indicating the success of the opertation.
   */
  async deleteNoteAsync(note: Note | string): Promise<boolean> {
    // Send the request to delete the note
    const id = typeof note === "string" ? note : note.id;
    const url = this.appendToUrl(id);
    const response = await firstValueFrom(this.http.delete(url, { headers: this.httpHeaders, observe: 'response' }));

    // Handle the response
    if (response instanceof HttpErrorResponse) {
      this.handleErrorAsync(new Error(response.message), 'deleteNoteAsync');
    } else {
      this.log(`deleteNoteAsync: Deleted note with ID = ${id}`);
    }

    // Return the result
    return response.ok;
  }

  /**
   * Appends the given string to the base notes URL.
   * @param str The string to add to the url.
   * @returns The transformed URL with the given str added.
   */
  private appendToUrl(str: string): string {
    return `${this.notesUrl}/${str}`;
  }

  /**
   * Labels and logs a message.
   * @param message The message to log.
   */
  private log(message: string): void {
    const labeledMessage = `NoteService: ${message}`;
    console.log(labeledMessage)
  }

  /**
   * Handles a failed HTTP operation and allows the app to continue.
   * @param operation The name of the operation that failed.
   * @param result Optional value to return as the observable result.
   */
  private handleError<T>(operation = "operation", result?: T) {
    return (error: any): Observable<T> => {
      // TODO: Send the error to remote logging infrastructure
      console.error(error);

      // TODO: Improve error transformation for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Return an empty result to allow the app to continue running
      return of(result as T);
    }
  }
  
  /**
   * Handles a failed HTTP operation and allows the app to continue.
   * @param error The error that occurred.
   * @param operation The name of the operation that failed.
   * @param result Optional value to return as a Promise.
   * @returns A Promise of the given result.
   */
  private handleErrorAsync<T>(error: any, operation = "operation", result?: T): Promise<T> {
    // TODO: Send the error to remote logging infrastructure
    console.error(error);

    // TODO: Improve error transformation for user consumption
    this.log(`${operation} failed: ${error.message}`);

    // Return an empty result to allow the app to continue running
    return Promise.resolve(result as T);
  }
}
