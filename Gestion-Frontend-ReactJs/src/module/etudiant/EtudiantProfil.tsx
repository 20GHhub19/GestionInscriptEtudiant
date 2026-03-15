export const EtudiantProfil = () => {
  return (
    <section className='space-y-6'>
      <div>
        <h1 className='text-3xl font-bold text-[#09173c]'>Mon profil</h1>
        <p className='mt-2 text-slate-600'>
          Consultez vos informations personnelles et académiques.
        </p>
      </div>

      <div className='grid grid-cols-1 gap-5 lg:grid-cols-2'>
        <div className='rounded-2xl border border-slate-200 bg-white p-6 shadow-sm'>
          <h2 className='text-xl font-bold text-[#09173c]'>
            Informations personnelles
          </h2>

          <div className='mt-5 space-y-4'>
            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Nom complet</p>
              <p className='font-semibold text-[#09173c]'>Jean Dupont</p>
            </div>

            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Courriel</p>
              <p className='font-semibold text-[#09173c]'>jean.dupont@yahoo.com</p>
            </div>

            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Téléphone</p>
              <p className='font-semibold text-[#09173c]'>(514)-123-4567</p>
            </div>

            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Adresse</p>
              <p className='font-semibold text-[#09173c]'>
                123 Rue Principale, Montréal, QC
              </p>
            </div>
          </div>
        </div>

        <div className='rounded-2xl border border-slate-200 bg-white p-6 shadow-sm'>
          <h2 className='text-xl font-bold text-[#09173c]'>
            Informations académiques
          </h2>

          <div className='mt-5 space-y-4'>
            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Matricule</p>
              <p className='font-semibold text-[#09173c]'>20260100</p>
            </div>

            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Programme</p>
              <p className='font-semibold text-[#09173c]'>Informatique</p>
            </div>

            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Statut</p>
              <p className='font-semibold text-green-700'>Actif</p>
            </div>

            <div className='rounded-xl bg-slate-50 px-4 py-3'>
              <p className='text-sm text-slate-500'>Date d’inscription</p>
              <p className='font-semibold text-[#09173c]'>2026-01-10</p>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}