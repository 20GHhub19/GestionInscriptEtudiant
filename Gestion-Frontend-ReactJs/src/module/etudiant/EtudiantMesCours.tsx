export const EtudiantMesCours = () => {
  const cours = [
    {
      id: 1,
      code: 'IFT101',
      nom: 'Introduction à la programmation',
      enseignant: 'Marc Tremblay',
      horaire: 'Lun 09:00 - 12:00',
      salle: 'B-204',
      statut: 'En cours',
    },
    {
      id: 2,
      code: 'IFT202',
      nom: 'Bases de données',
      enseignant: 'Julie Gagnon',
      horaire: 'Mar 13:00 - 16:00',
      salle: 'C-310',
      statut: 'En cours',
    },
    {
      id: 3,
      code: 'IFT310',
      nom: 'Développement Web',
      enseignant: 'Antoine Lefebvre',
      horaire: 'Jeu 10:00 - 13:00',
      salle: 'Lab-12',
      statut: 'En cours',
    },
  ]

  return (
    <section className='space-y-6'>
      <div>
        <h1 className='text-3xl font-bold text-[#09173c]'>Mes cours</h1>
        <p className='mt-2 text-slate-600'>
          Consultez les cours auxquels vous êtes inscrit pour la session en cours.
        </p>
      </div>

      <div className='rounded-2xl border border-slate-200 bg-white p-4 shadow-sm'>
        <input
          type='text'
          placeholder='Rechercher un cours...'
          className='w-full rounded-xl border border-slate-300 px-4 py-3 outline-none focus:ring-2 focus:ring-[#0f255e]'
        />
      </div>

      <div className='overflow-hidden rounded-2xl border border-slate-200 bg-white shadow-sm'>
        <div className='overflow-x-auto'>
          <table className='min-w-full text-left'>
            <thead className='bg-slate-50 text-sm text-slate-600'>
              <tr>
                <th className='px-6 py-4 font-semibold'>Code</th>
                <th className='px-6 py-4 font-semibold'>Cours</th>
                <th className='px-6 py-4 font-semibold'>Enseignant</th>
                <th className='px-6 py-4 font-semibold'>Horaire</th>
                <th className='px-6 py-4 font-semibold'>Salle</th>
                <th className='px-6 py-4 font-semibold'>Statut</th>
              </tr>
            </thead>

            <tbody className='divide-y divide-slate-200 text-sm text-slate-700'>
              {cours.map((coursItem) => (
                <tr key={coursItem.id} className='hover:bg-slate-50'>
                  <td className='px-6 py-4 font-medium text-[#09173c]'>
                    {coursItem.code}
                  </td>
                  <td className='px-6 py-4'>{coursItem.nom}</td>
                  <td className='px-6 py-4'>{coursItem.enseignant}</td>
                  <td className='px-6 py-4'>{coursItem.horaire}</td>
                  <td className='px-6 py-4'>{coursItem.salle}</td>
                  <td className='px-6 py-4'>
                    <span className='rounded-full bg-green-100 px-3 py-1 text-xs font-semibold text-green-700'>
                      {coursItem.statut}
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