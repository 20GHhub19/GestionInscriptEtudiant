import { etudiants } from "../../data/etudiantData"




export const AdminEtudiants = () => {

  return (
    <section className="space-y-6">
      <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div>
          <h1 className="text-3xl font-bold text-[#09173c]">Étudiants</h1>
          <p className="mt-2 text-slate-600">
            Gérez les étudiants inscrits dans le système.
          </p>
        </div>

        <button className="rounded-xl bg-[#0f255e] px-5 py-3 font-semibold text-white transition hover:bg-[#16337a]">
          Ajouter un étudiant
        </button>
      </div>

      <div className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
        <input
          type="text"
          placeholder="Rechercher un étudiant..."
          className="w-full rounded-xl border border-slate-300 px-4 py-3 outline-none focus:ring-2 focus:ring-[#0f255e]"
        />
      </div>

      <div className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
        <div className="overflow-x-auto">
          <table className="min-w-full text-left">
            <thead className="bg-slate-50 text-sm text-slate-600">
              <tr>
                <th className="px-6 py-4 font-semibold">Matricule</th>
                <th className="px-6 py-4 font-semibold">Nom</th>
                <th className="px-6 py-4 font-semibold">Prénom</th>
                <th className="px-6 py-4 font-semibold">Courriel</th>
                <th className="px-6 py-4 font-semibold">Statut</th>
                <th className="px-6 py-4 font-semibold">Programme</th>
                <th className="px-6 py-4 font-semibold">Actions</th>
              </tr>
            </thead>

            <tbody className="divide-y divide-slate-200 text-sm text-slate-700">
              {etudiants.map((etudiant) => (
                <tr key={etudiant.id} className="hover:bg-slate-50">
                  <td className="px-6 py-4">{etudiant.matricule}</td>
                  <td className="px-6 py-4">{etudiant.nom}</td>
                  <td className="px-6 py-4">{etudiant.prenom}</td>
                  <td className="px-6 py-4">{etudiant.courriel}</td>
                  <td className="px-6 py-4">
                    <span className="rounded-full bg-green-100 px-3 py-1 text-xs font-semibold text-green-700">
                      {etudiant.statut}
                    </span>
                  </td>
                  <td className="px-6 py-4">{etudiant.programme}</td>
                  <td className="px-6 py-4">
                    <div className="flex gap-2">
                      <button className="rounded-lg bg-slate-100 px-3 py-2 text-sm font-medium text-[#09173c] hover:bg-slate-200">
                        Voir
                      </button>
                      <button className="rounded-lg bg-[#0f255e] px-3 py-2 text-sm font-medium text-white hover:bg-[#16337a]">
                        Modifier
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </section>
  )
}