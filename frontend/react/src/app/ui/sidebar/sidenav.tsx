import NavLinks from '@/app/ui/sidebar/nav-links';
import styles from "@/app/ui/sidebar/sidebar.module.scss";

// TODO: Parameterize links and pass to NavLinks
export default function SideNav({ isCollapsed }: { isCollapsed: boolean }) {
  return (
    <nav className={`h-100 d-flex flex-column flex-shrink-0 p-2 text-bg-dark ${styles.expand_horizontal} ${(isCollapsed) ? styles.min : styles.max}`}>
      <NavLinks isCollapsed={isCollapsed} />
    </nav>
  );
}
