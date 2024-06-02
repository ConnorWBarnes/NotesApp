import { Injectable } from '@angular/core';

import { Note } from './note';
import { NOTES } from './mock-notes';

@Injectable({
  providedIn: 'root'
})
export class NoteService {

  constructor() { }

  getNotes(): Note[] {
    return NOTES;
  }
}
