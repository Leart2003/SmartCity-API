import { Routes, Route } from "react-router-dom";
import Navbar from "./components/layout/Navbar";
import Footer from "./components/layout/Footer";
import HomePage from "./pages/HomePage";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import FavoritesPage from "./pages/FavoritesPage";
import PlaceDetailsPage from "./pages/PlaceDetailsPage";
import AdminDashboardPage from "./pages/AdminDashboardPage";
import ProtectedRoute from "./components/common/ProtectedRoute";

function App() {
  return (
<div className="d-flex flex-column min-vh-100">
   <Navbar/>
  <main className="flex-grow-1">
    <Routes>
       <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/places/:id" element={<PlaceDetailsPage />} />

          <Route
            path="/favorites"
            element={
              <ProtectedRoute>
                <FavoritesPage />
              </ProtectedRoute>
            }
          />

          <Route
            path="/admin"
            element={
              <ProtectedRoute requireAdmin={true}>
                <AdminDashboardPage />
              </ProtectedRoute>
            }
          />
    </Routes>
  </main>
    <Footer />
</div>
  );
}

export default App;
