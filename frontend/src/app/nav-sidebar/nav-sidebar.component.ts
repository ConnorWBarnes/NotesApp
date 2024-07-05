import { NgFor } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-nav-sidebar',
  standalone: true,
  imports: [RouterModule, NgFor],
  templateUrl: './nav-sidebar.component.html',
  styleUrl: './nav-sidebar.component.scss'
})
export class NavSidebarComponent {
  navLinks = [
    {
      routerLink: "/notes",
      displayText: "Notes",
      iconClass: "bi-journal-text"
    },
    {
      routerLink: "/create",
      displayText: "Create",
      iconClass: "bi-journal-plus"
    },
    {
      routerLink: "/archive",
      displayText: "Archive",
      iconClass: "bi-archive"
    },
    {
      routerLink: "/trash",
      displayText: "Trash",
      iconClass: "bi-trash"
    }
  ];

  @Input() isCollapsed!: boolean;
}
