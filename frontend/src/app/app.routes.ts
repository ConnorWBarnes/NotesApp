import { Routes } from '@angular/router';

import { NoteEditComponent } from './note/note-edit/note-edit.component';
import { NoteGridComponent } from './note/note-grid/note-grid.component';

export const routes: Routes = [
  {
    path: 'note/:id',
    component: NoteEditComponent
  },
  {
    path: '',
    component: NoteGridComponent
  }
];
