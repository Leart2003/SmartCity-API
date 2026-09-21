import { Link } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

function Navbar() {
  const auth = useAuth();

  function handleLogout() {
    auth.logout();
  }

  return (
    <nav className="navbar">
      <div className="navbar-left">
        <Link to="/" className="navbar-logo">
          ExploreKosova
        </Link>
      </div>

      <div className="navbar-right">
        <Link to="/">Home</Link>

        {auth.isAuthenticated && <Link to="/favorites">Favorites</Link>}

        {auth.isAdmin && <Link to="/admin">Admin</Link>}

        {auth.isAuthenticated ? (
          <>
            <span className="navbar-username">Hi, {auth.user.fullName}</span>
            <button onClick={handleLogout}>Logout</button>
          </>
        ) : (
          <>
            <Link to="/login">Login</Link>
            <Link to="/register">Register</Link>
          </>
        )}
      </div>
    </nav>
  );
}

export default Navbar;