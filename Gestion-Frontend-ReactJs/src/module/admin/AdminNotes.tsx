import { notes } from "../../data/notesData"

export const AdminNotes = () => {
  
  return (
    <section className="space-y-6">
      <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
        <div>
          <h1 className="text-3xl font-bold text-[#09173c]">Notes</h1>
          <p className="mt-2 text-slate-600">
            Gérez les notes attribuées aux évaluations des étudiants.
          </p>
        </div>

        <button className="rounded-xl bg-[#0f255e] px-5 py-3 font-semibold text-white transition hover:bg-[#16337a]">
          Ajouter une note
        </button>
      </div>

      <div className="rounded-2xl border border-slate-200 bg-white p-4 shadow-sm">
        <input
          type="text"
          placeholder="Rechercher une note..."
          className="w-full rounded-xl border border-slate-300 px-4 py-3 outline-none focus:ring-2 focus:ring-[#0f255e]"
        />
      </div>

      <div className="overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm">
        <div className="overflow-x-auto">
          <table className="min-w-full text-left">
            <thead className="bg-slate-50 text-sm text-slate-600">
              <tr>
                <th className="px-6 py-4 font-semibold">Étudiant</th>
                <th className="px-6 py-4 font-semibold">Évaluation</th>
                <th className="px-6 py-4 font-semibold">Cours</th>
                <th className="px-6 py-4 font-semibold">Note numérique</th>
                <th className="px-6 py-4 font-semibold">Note lettre</th>
                <th className="px-6 py-4 font-semibold">Actions</th>
              </tr>
            </thead>

            <tbody className="divide-y divide-slate-200 text-sm text-slate-700">
              {notes.map((note) => (
                <tr key={note.id} className="hover:bg-slate-50">
                  <td className="px-6 py-4 font-medium text-[#09173c]">
                    {note.etudiant}
                  </td>
                  <td className="px-6 py-4">{note.evaluation}</td>
                  <td className="px-6 py-4">{note.cours}</td>
                  <td className="px-6 py-4">{note.noteNumerique}</td>
                  <td className="px-6 py-4">
                    <span className="rounded-full bg-blue-100 px-3 py-1 text-xs font-semibold text-blue-700">
                      {note.noteLettre}
                    </span>
                  </td>
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