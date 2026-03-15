export const AdminCours = () => {
  const cours = [
    {
      id: 1,
      code: "IFT101",
      nom: "Introduction à la programmation",
      credit: 3,
      type: "Théorique",
      heures: 45,
    },
    {
      id: 2,
      code: "IFT202",
      nom: "Bases de données",
      credit: 4,
      type: "Laboratoire",
      heures: 60,
    },
    {
      id: 3,
      code: "IFT310",
      nom: "Développement Web",
      credit: 3,
      type: "Travaux pratiques",
      heures: 45,
    },
  ]

  return (
    <section className="space-y-6">

      <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div>
          <h1 className="text-3xl font-bold text-[#09173c]">Cours</h1>
          <p className="mt-2 text-slate-600">
            Gérez les cours disponibles dans les programmes.
          </p>
        </div>

        <button className="rounded-xl bg-[#0f255e] px-5 py-3 font-semibold text-white hover:bg-[#16337a] transition">
          Ajouter un cours
        </button>
      </div>

      <div className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
        <input
          type="text"
          placeholder="Rechercher un cours..."
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
                <th className="px-6 py-4 font-semibold">Crédits</th>
                <th className="px-6 py-4 font-semibold">Type</th>
                <th className="px-6 py-4 font-semibold">Heures</th>
                <th className="px-6 py-4 font-semibold">Actions</th>
              </tr>
            </thead>

            <tbody className="divide-y divide-slate-200 text-sm text-slate-700">

              {cours.map((c) => (
                <tr key={c.id} className="hover:bg-slate-50">

                  <td className="px-6 py-4 font-medium text-[#09173c]">
                    {c.code}
                  </td>

                  <td className="px-6 py-4">{c.nom}</td>

                  <td className="px-6 py-4">{c.credit}</td>

                  <td className="px-6 py-4">
                    <span className="rounded-full bg-blue-100 px-3 py-1 text-xs font-semibold text-blue-700">
                      {c.type}
                    </span>
                  </td>

                  <td className="px-6 py-4">{c.heures}</td>

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