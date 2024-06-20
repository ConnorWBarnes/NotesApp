import { Routes } from '@angular/router';

import { NoteEditComponent } from './note/note-edit/note-edit.component';
import { NoteGridComponent } from './note/note-grid/note-grid.component';
import { noteResolver } from './note/note.resolver';

export const routes: Routes = [
  {
    path: 'note/:id',
    component: NoteEditComponent,
    resolve: { note: noteResolver }
  },
  {
    path: '',
    component: NoteGridComponent
  }
];
