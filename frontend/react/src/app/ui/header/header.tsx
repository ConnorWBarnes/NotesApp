export default function Header({ toggleCollapsed }: { toggleCollapsed: () => void }) {
  return (
    <header className="fixed-top bg-dark d-flex flex-wrap justify-content-center py-1">
      <div className="container-fluid d-grid gap-3 align-items-center" style={{gridTemplateColumns: '1fr 2fr 1fr'}}>
        {/* Sidebar button, Logo, and title */}
        <div className="d-flex gap-2">
          <button type="button" onClick={toggleCollapsed} className="btn btn-dark" aria-label="Main menu">
            <i className="bi bi-list" style={{fontSize: '2rem'}}></i>
          </button>
          <a href="/"
             className="d-flex align-items-center mb-3 mb-md-0 me-md-auto link-body-emphasis text-decoration-none">
            {/*<svg className="bi me-2" width="40" height="32"><use xlink:href="#bootstrap"></use></svg>*/}
            <i className="bi bi-journals me-2" style={{fontSize: '2rem'}}></i>
            <span className="fs-4">Notes App</span>
          </a>
        </div>
        {/* Search form */}
        <div className="d-flex align-items-center">
          <form className="w-100 me-3" role="search">
            <input type="search" className="form-control app-search" placeholder="Search..." aria-label="Search"></input>
          </form>
        </div>
        {/* Authentication */}
        <div className="text-end">
          {/* TODO: Implement authentication component */}

        </div>
      </div>
    </header>
  );
}
