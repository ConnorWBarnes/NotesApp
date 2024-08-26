import Link from 'next/link';
import NavLinks from '@/app/ui/sidebar/nav-links';

// TODO: Parameterize links and pass to NavLinks
export default function SideNav({ isCollapsed }: { isCollapsed: boolean }) {
  return (
    <nav className={`h-100 d-flex flex-column flex-shrink-0 p-2 text-bg-dark expand-horizontal ${(isCollapsed) ?'min':'max'}`}>
      <NavLinks isCollapsed={isCollapsed} />
    </nav>
  );
}
