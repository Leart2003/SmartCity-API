import { Navigate } from "react-router-dom";
import { useAuth } from "../../context/authContext";



function ProtectedRoute({ children, requireAdmin }) {
  const auth = useAuth();

  if (!auth.isAuthenticated) {
    return <Navigate to="/login" />;
  }

  if (requireAdmin && !auth.isAdmin) {
    return <Navigate to="/" />;
  }

  return children;
}

export default ProtectedRoute;