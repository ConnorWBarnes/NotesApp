// Must be a client component to use the 'useState()' hook
'use client';

import React from "react";
import Header from "@/app/ui/header/header";
import SideNav from "@/app/ui/sidebar/sidenav";

export default function CustomLayout({ children }: { children: React.ReactNode }) {
  const [isCollapsed, setCollapsed] = React.useState(false);

  function toggleCollapsed() {
    setCollapsed(!isCollapsed);
  }

  return (
    <>
      <Header toggleCollapsed={toggleCollapsed} />
      <main className="overflow-hidden">
        <div className="container-fluid min-vh-100 d-flex flex-column">
          <div className="row border-bottom">
            <div className="col">
              <div className="container-fluid" style={{height: '4.5rem'}}>
                This container acts as padding without messing up the nav bar or router outlet
              </div>
            </div>
          </div>
          <div className="row flex-grow-1">
            <div className="col-md-auto p-0">
              <SideNav isCollapsed={isCollapsed} />
            </div>
            <div className="col p-0">
              {children}
            </div>
          </div>
        </div>
      </main>
    </>
  );
}
