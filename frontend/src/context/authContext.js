import { createContext, useContext, useState, useEffect } from "react";
import { login as loginApi, register as registerApi } from "../api/authApi";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);

  // Kur aplikacioni niset, kontrollo nëse ka user të ruajtur në localStorage
  useEffect(() => {
    const storedUser = localStorage.getItem("user");

    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  async function login(credentials) {
    const data = await loginApi(credentials);

    localStorage.setItem("token", data.token);
    localStorage.setItem("user", JSON.stringify(data));
    setUser(data);
  }

  async function register(registerData) {
    const data = await registerApi(registerData);

    localStorage.setItem("token", data.token);
    localStorage.setItem("user", JSON.stringify(data));
    setUser(data);
  }

  function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
    setUser(null);
  }

  const isAuthenticated = user !== null;
  const isAdmin = user !== null && user.role === "Admin";

  const value = {
    user: user,
    login: login,
    register: register,
    logout: logout,
    isAuthenticated: isAuthenticated,
    isAdmin: isAdmin,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  return useContext(AuthContext);
}