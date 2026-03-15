import { createBrowserRouter, Navigate } from "react-router-dom"
import { Login } from "../module/auth/Login"
import { Register } from "../module/auth/Register"
import { AuthLayout } from "../module/auth/layout/AuthLayout"
import { AdminLayout } from "../module/admin/layout/AdminLayout"
import { AdminDashboard } from "../module/admin/AdminDashboard"
import { AdminProgrammes } from "../module/admin/AdminProgrammes"
import { AdminEtudiants } from "../module/admin/AdminEtudiants"
import { AdminCours } from "../module/admin/AdminCours"
import { AdminProfesseurs } from "../module/admin/AdminProfesseurs"
import { AdminInscriptions } from "../module/admin/AdminInscriptions"
import { AdminNotes } from "../module/admin/AdminNotes"
import { EtudiantLayout } from "../module/etudiant/layout/EtudiantLayout"
import { EtudiantDashboard } from "../module/etudiant/EtudiantDashboard"
import { EtudiantMesCours } from "../module/etudiant/EtudiantMesCours"
import { EtudiantMesNotes } from "../module/etudiant/EtudiantMesNotes"
import { EtudiantProfil } from "../module/etudiant/EtudiantProfil"



export const router = createBrowserRouter([
  {
    path: "/",
    element: <Navigate to="/auth/login" replace />,
  },
  {
    path: "/auth",
    element: <AuthLayout />,
    children: [
      {
        index: true,
        element: <Navigate to="login" replace />,
      },
      {
        path: "login",
        element: <Login />,
      },
      {
        path: "register",
        element: <Register />,
      },
    ],
  },
  {
    path: "/admin",
    element: <AdminLayout />,
    children: [
      {
        index: true,
        element: <Navigate to="dashboard" replace />,
      },
      {
        path: "dashboard",
        element: <AdminDashboard />,
      },
      {
        path: "programmes",
        element: <AdminProgrammes />,
      },
      {
        path: "etudiants",
        element: <AdminEtudiants />,
      },
      {
        path: "cours",
        element: <AdminCours />,
      },
      {
        path: "professeurs",
        element: <AdminProfesseurs />,
      },
      {
        path: "inscriptions",
        element: <AdminInscriptions />,
      },
      {
        path: "notes",
        element: <AdminNotes />,
      },
    ],
  },
  {
    path: "/etudiant",
    element: <EtudiantLayout />,
    children: [
      {
        index: true,
        element: <Navigate to="dashboard" replace />,
      },
      {
        path: "dashboard",
        element: <EtudiantDashboard />,
      },
      {
        path: "mes-cours",
        element: <EtudiantMesCours />,
      },
      {
        path: "mes-notes",
        element: <EtudiantMesNotes />,
      },
      {
        path: "profil",
        element: <EtudiantProfil />,
      },
    ],
  },
])