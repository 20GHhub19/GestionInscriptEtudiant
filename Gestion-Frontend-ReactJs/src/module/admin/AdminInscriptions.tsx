import { inscriptions } from "../../data/inscriptionData"

export const AdminInscriptions = () => {
  

  return (
    <section className="space-y-6">
      <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div>
          <h1 className="text-3xl font-bold text-[#09173c]">Inscriptions</h1>
          <p className="mt-2 text-slate-600">
            Gérez les inscriptions des étudiants aux cours offerts.
          </p>
        </div>

        <button className="rounded-xl bg-[#0f255e] px-5 py-3 font-semibold text-white transition hover:bg-[#16337a]">
          Ajouter une inscription
        </button>
      </div>

      <div className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
        <input
          type="text"
          placeholder="Rechercher une inscription..."
          className="w-full rounded-xl border border-slate-300 px-4 py-3 outline-none focus:ring-2 focus:ring-[#0f255e]"
        />
      </div>

      <div className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
        <div className="overflow-x-auto">
          <table className="min-w-full text-left">
            <thead className="bg-slate-50 text-sm text-slate-600">
              <tr>
                <th className="px-6 py-4 font-semibold">Étudiant</th>
                <th className="px-6 py-4 font-semibold">Cours</th>
                <th className="px-6 py-4 font-semibold">Date d'inscription</th>
                <th className="px-6 py-4 font-semibold">Statut</th>
                <th className="px-6 py-4 font-semibold">Tentative</th>
                <th className="px-6 py-4 font-semibold">Actions</th>
              </tr>
            </thead>

            <tbody className="divide-y divide-slate-200 text-sm text-slate-700">
              {inscriptions.map((inscription) => (
                <tr key={inscription.id} className="hover:bg-slate-50">
                  <td className="px-6 py-4 font-medium text-[#09173c]">
                    {inscription.etudiant}
                  </td>
                  <td className="px-6 py-4">{inscription.cours}</td>
                  <td className="px-6 py-4">{inscription.date}</td>
                  <td className="px-6 py-4">
                    <span
                      className={`rounded-full px-3 py-1 text-xs font-semibold ${
                        inscription.statut === "Confirmée"
                          ? "bg-green-100 text-green-700"
                          : "bg-yellow-100 text-yellow-700"
                      }`}
                    >
                      {inscription.statut}
                    </span>
                  </td>
                  <td className="px-6 py-4">{inscription.tentative}</td>
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