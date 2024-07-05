import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { NavSidebarComponent } from './nav-sidebar/nav-sidebar.component';
import { NoteCreateComponent } from './note/note-create/note-create.component';
import { NoteEditComponent } from './note/note-edit/note-edit.component';
import { NoteGridComponent } from './note/note-grid/note-grid.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    NavSidebarComponent,
    NoteCreateComponent,
    NoteEditComponent,
    NoteGridComponent,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Notes App';
  isCollapsed = false; // TODO: Restore state from previous session?
}
