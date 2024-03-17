import { Link, Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <div>
      <h1 className='font-black'>Translation Management</h1>

      {/* A "layout route" is a good place to put markup you want to
          share across all the pages on your site, like navigation. */}
      <nav>
        <ul>
          <li className="m-4">
            <Link to="/">Home</Link>
           </li>
          <li className="m-4">
            <Link to="/translators">Translators</Link>
           </li>
        </ul>
      </nav>

      <hr className="my-4" />

      <Outlet />
    </div>
  )
}

export default Layout;