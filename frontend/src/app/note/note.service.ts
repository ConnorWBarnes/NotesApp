import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Note } from './note';

/**
 * Service for managing notes.
 */
@Injectable({
  providedIn: 'root'
})
export class NoteService {
  notesUrl = 'http://localhost:3000/notes';
  
  httpOptions = {
    headers: new HttpHeaders({"Content-Type": "application/json"})
  };

  constructor(private http: HttpClient) { }
  
  /**
   * GET: Gets all notes from the server.
   */
  getNotes$(): Observable<Note[]> {
    return this.http.get<Note[]>(this.notesUrl).pipe(
        tap(notes => this.log(`getNotes$: Retrieved ${notes.length} notes`)),
        catchError(this.handleError<Note[]>('getNotes$', []))
      );
  }

  /**
   * GET: Gets a note by ID. Will 404 if not found.
   * @param id The ID of the note to get.
   */
  getNote$(id: string): Observable<Note> {
    const url = `${this.notesUrl}/${id}`;
    return this.http.get<Note>(url).pipe(
      tap(_ => this.log(`getNote$: Retrieved note ID = ${id}`)),
      catchError(this.handleError<Note>(`getNote$ ID = ${id}`))
    );
  }

  /**
   * GET: Gets a note by ID. Returns 'undefined' when ID not found.
   * @param id The ID of the Note to get.
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
   */
  createNote$(note: Note): Observable<Note> {
    return this.http.post<Note>(this.notesUrl, note, this.httpOptions).pipe(
      tap((newNote: Note) => this.log(`createNote$: Created note with ID = ${newNote.id}`)),
      catchError(this.handleError<Note>('createNote$'))
    );
  }

  /**
   * PUT: Updates the note on the server.
   * @param note The updated note.
   */
  updateNote$(note: Note): Observable<any> {
    return this.http.put(this.notesUrl, note, this.httpOptions).pipe(
      tap(_ => this.log(`Updated note with ID = ${note.id}`)),
      catchError(this.handleError<any>('updateNote$'))
    );
  }

  /**
   * DELETE: Deletes a note from the server.
   * @param note The note or the ID of the note to delete.
   */
  deleteNote$(note: Note | string): Observable<Note> {
    const id = typeof note === "string" ? note : note.id;
    const url = `${this.notesUrl}/${id}`;

    return this.http.delete<Note>(url, this.httpOptions).pipe(
      tap(_ => this.log(`deleteNote$: Deleted note ID = ${id}`)),
      catchError(this.handleError<Note>('deleteNote$'))
    );
  }

  /**
   * Labels and logs a message.
   * @param message The message to log.
   */
  private log(message: string) {
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
}
