// NavLinks must be a Client Component to use the 'usePathname()' hook
'use client';

// Map of links to display in the side navigation.
// Depending on the size of the application, this would be stored in a database.
import Link from "next/link";
import { usePathname } from "next/navigation";
import clsx from "clsx";

const links = [
  {
    name: "Notes",
    href: "/Notes",
    iconClass: "bi-journal-text",
    svgFile: "journal-text"
  },
  {
    name: "Create",
    href: "/Create",
    iconClass: "bi-journal-plus",
    svgFile: "journal-plus"
  },
  {
    name: "Archive",
    href: "/Archive",
    iconClass: "bi-archive",
    svgFile: "archive"
  },
  {
    name: "Trash",
    href: "/Trash",
    iconClass: "bi-trash",
    svgFile: "trash"
  }
];

export default function NavLinks({ isCollapsed }: { isCollapsed: boolean }) {
  const pathname = usePathname();
  return (
    <ul className="nav nav-pills flex-column mb-auto">
      {links.map((link) => {
        return (
          <li key={link.name} className="nav-item">
            <Link href={link.href} className={clsx('nav-link text-white', {'active': pathname === link.href})}>
              <i className={`bi ${link.iconClass} ${isCollapsed ? '' : 'me-2'}`} style={{fontSize: "1rem"}}></i>
              {!isCollapsed ? (link.name) : null}
            </Link>
          </li>
        );
      })}
    </ul>
  );
}
