import { programmes } from "../../data/programmesData"

export const AdminProgrammes = () => {
  

  return (
    <section className="space-y-6">
      <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div>
          <h1 className="text-3xl font-bold text-[#09173c]">Programmes</h1>
          <p className="mt-2 text-slate-600">
            Gérez les programmes académiques disponibles.
          </p>
        </div>

        <button className="rounded-xl bg-[#0f255e] px-5 py-3 font-semibold text-white transition hover:bg-[#16337a]">
          Ajouter un programme
        </button>
      </div>

      <div className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
        <input
          type="text"
          placeholder="Rechercher un programme..."
          className="w-full rounded-xl border border-slate-300 px-4 py-3 outline-none focus:ring-2 focus:ring-[#0f255e]"
        />
      </div>

      <div className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
        <div className="overflow-x-auto">
          <table className="min-w-full text-left">
            <thead className="bg-slate-50 text-sm text-slate-600">
              <tr>
                <th className="px-6 py-4 font-semibold">Code</th>
                <th className="px-6 py-4 font-semibold">Nom</th>
                <th className="px-6 py-4 font-semibold">Description</th>
                <th className="px-6 py-4 font-semibold">Crédits</th>
                <th className="px-6 py-4 font-semibold">Unité</th>
                <th className="px-6 py-4 font-semibold">Durée</th>
                <th className="px-6 py-4 font-semibold">Actions</th>
              </tr>
            </thead>

            <tbody className="divide-y divide-slate-200 text-sm text-slate-700">
              {programmes.map((programme) => (
                <tr key={programme.id} className="hover:bg-slate-50">
                  <td className="px-6 py-4 font-medium text-[#09173c]">
                    {programme.code}
                  </td>
                  <td className="px-6 py-4">{programme.nom}</td>
                  <td className="px-6 py-4">{programme.description}</td>
                  <td className="px-6 py-4">{programme.credits}</td>
                  <td className="px-6 py-4">{programme.unite}</td>
                  <td className="px-6 py-4">{programme.duree} ans</td>
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