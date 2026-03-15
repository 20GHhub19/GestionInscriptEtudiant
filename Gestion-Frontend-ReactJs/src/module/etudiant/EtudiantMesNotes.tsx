export const EtudiantMesNotes = () => {
  const notes = [
    {
      id: 1,
      cours: 'Bases de données',
      evaluation: 'Intra 1',
      noteNumerique: 85.5,
      noteLettre: 'A',
      date: '2026-02-10',
    },
    {
      id: 2,
      cours: 'Développement Web',
      evaluation: 'Quiz 2',
      noteNumerique: 72,
      noteLettre: 'B',
      date: '2026-02-18',
    },
    {
      id: 3,
      cours: 'Introduction à la programmation',
      evaluation: 'Examen final',
      noteNumerique: 91,
      noteLettre: 'A+',
      date: '2026-03-01',
    },
  ]

  return (
    <section className='space-y-6'>
      <div>
        <h1 className='text-3xl font-bold text-[#09173c]'>Mes notes</h1>
        <p className='mt-2 text-slate-600'>
          Consultez les résultats de vos évaluations et votre rendement académique.
        </p>
      </div>

      <div className='rounded-2xl border border-slate-200 bg-white p-4 shadow-sm'>
        <input
          type='text'
          placeholder='Rechercher une note...'
          className='w-full rounded-xl border border-slate-300 px-4 py-3 outline-none focus:ring-2 focus:ring-[#0f255e]'
        />
      </div>

      <div className='overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm'>
        <div className='overflow-x-auto'>
          <table className='min-w-full text-left'>
            <thead className='bg-slate-50 text-sm text-slate-600'>
              <tr>
                <th className='px-6 py-4 font-semibold'>Cours</th>
                <th className='px-6 py-4 font-semibold'>Évaluation</th>
                <th className='px-6 py-4 font-semibold'>Date</th>
                <th className='px-6 py-4 font-semibold'>Note numérique</th>
                <th className='px-6 py-4 font-semibold'>Note lettre</th>
              </tr>
            </thead>

            <tbody className='divide-y divide-slate-200 text-sm text-slate-700'>
              {notes.map((note) => (
                <tr key={note.id} className='hover:bg-slate-50'>
                  <td className='px-6 py-4 font-medium text-[#09173c]'>
                    {note.cours}
                  </td>
                  <td className='px-6 py-4'>{note.evaluation}</td>
                  <td className='px-6 py-4'>{note.date}</td>
                  <td className='px-6 py-4'>{note.noteNumerique}</td>
                  <td className='px-6 py-4'>
                    <span className='rounded-full bg-blue-100 px-3 py-1 text-xs font-semibold text-blue-700'>
                      {note.noteLettre}
                    </span>
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