// NavLinks must be a Client Component to use the 'usePathname()' hook
'use client';

import Link from 'next/link';
import { usePathname } from 'next/navigation';
import styles from '@/components/sidebar/sidebar.module.scss';

// Map of links to display in the side navigation.
// Depending on the size of the application, this would be stored in a database.
const notesUrl = '/notes';
const links = [
  {
    name: 'Notes',
    href: `${notesUrl}`,
    iconClass: 'bi-journal-text',
    svgFile: 'journal-text'
  },
  {
    name: 'Create',
    href: `${notesUrl}/create`,
    iconClass: 'bi-journal-plus',
    svgFile: 'journal-plus'
  },
  {
    name: 'Archive',
    href: `${notesUrl}/archive`,
    iconClass: 'bi-archive',
    svgFile: 'archive'
  },
  {
    name: 'Trash',
    href: `${notesUrl}/trash`,
    iconClass: 'bi-trash',
    svgFile: 'trash'
  }
];

/**
 * Determines whether the given pathname and href are the same string (ignoring case).
 * @param pathname The current URL's pathname.
 * @param href The href of the navigation link.
 * @returns A flag indicating whether the given pathname and href are the same string (ignoring case).
 */
function linkIsActive(pathname: string, href: string): boolean {
  return pathname.localeCompare(href, undefined, { sensitivity: 'base' }) === 0;
}

export default function NavLinks({ isCollapsed }: { isCollapsed: boolean }) {
  const pathname = usePathname();
  return (
    <ul className='nav nav-pills flex-column mb-auto'>
      {links.map((link) => {
        return (
          <li key={link.name} className='nav-item'>
            <Link href={link.href} className={`nav-link text-white ${styles.sidebar_nav_link} ${linkIsActive(pathname, link.href) ? 'active' : ''}`}>
              <i className={`bi ${link.iconClass} ${isCollapsed ? '' : 'me-2'}`} style={{fontSize: '1rem'}}></i>
              {!isCollapsed ? (link.name) : null}
            </Link>
          </li>
        );
      })}
    </ul>
  );
}
