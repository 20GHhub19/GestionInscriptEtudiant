import { useNavigate } from "react-router-dom"




export const AdminDashboard = () => {

 



const navigate = useNavigate()


  return (
    <section className="space-y-6">
      <div>
        <h1 className="text-3xl font-bold text-[#09173c]">
          Tableau de bord
        </h1>
        <p className="mt-2 text-slate-600">
          Bienvenue dans l’espace administrateur du système de gestion des inscriptions étudiantes.
        </p>
      </div>

      <div className="grid grid-cols-1 gap-5 md:grid-cols-2 xl:grid-cols-4">
        <div className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p className="text-sm font-medium text-slate-500">Étudiants</p>
          <h2 className="mt-3 text-3xl font-bold text-[#09173c]">245</h2>
          <p className="mt-2 text-sm text-slate-500">Étudiants inscrits</p>
        </div>

        <div className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p className="text-sm font-medium text-slate-500">Cours</p>
          <h2 className="mt-3 text-3xl font-bold text-[#09173c]">38</h2>
          <p className="mt-2 text-sm text-slate-500">Cours disponibles</p>
        </div>

        <div className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p className="text-sm font-medium text-slate-500">Professeurs</p>
          <h2 className="mt-3 text-3xl font-bold text-[#09173c]">16</h2>
          <p className="mt-2 text-sm text-slate-500">Professeurs actifs</p>
        </div>

        <div className="rounded-2xl border border-slate-200 bg-white p-5 shadow-sm">
          <p className="text-sm font-medium text-slate-500">Inscriptions</p>
          <h2 className="mt-3 text-3xl font-bold text-[#09173c]">512</h2>
          <p className="mt-2 text-sm text-slate-500">Inscriptions enregistrées</p>
        </div>
      </div>

      <div className="grid grid-cols-1 gap-5 lg:grid-cols-2">
        <div className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
          <h3 className="text-xl font-bold text-[#09173c]">
            Actions rapides
          </h3>



          <div className="mt-5 grid grid-cols-1 gap-4 sm:grid-cols-2">

          <button
            onClick={() => navigate("/admin/etudiants")}
            className="rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]"
          >
            Ajouter un étudiant
          </button>

          <button
            onClick={() => navigate("/admin/cours")}
            className="rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]"
          >
            Ajouter un cours
          </button>

          <button
            onClick={() => navigate("/admin/professeurs")}
            className="rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]"
          >
            Ajouter un professeur
          </button>

          <button
            onClick={() => navigate("/admin/inscriptions")}
            className="rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]"
          >
            Voir les inscriptions
          </button>

        </div>
        </div>




        <div className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
          <h3 className="text-xl font-bold text-[#09173c]">
            Aperçu du système
          </h3>

          <div className="mt-5 space-y-4 text-slate-600">
            <div className="flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3">
              <span>Programmes actifs</span>
              <span className="font-bold text-[#09173c]">8</span>
            </div>

            <div className="flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3">
              <span>Semestres en cours</span>
              <span className="font-bold text-[#09173c]">2</span>
            </div>

            <div className="flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3">
              <span>Évaluations planifiées</span>
              <span className="font-bold text-[#09173c]">27</span>
            </div>

            <div className="flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3">
              <span>Spécialisations disponibles</span>
              <span className="font-bold text-[#09173c]">11</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}