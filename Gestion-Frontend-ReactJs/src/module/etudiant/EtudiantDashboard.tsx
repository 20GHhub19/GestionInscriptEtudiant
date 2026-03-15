import { useNavigate } from 'react-router-dom'

export const EtudiantDashboard = () => {
  const navigate = useNavigate()

  return (
    <section className='space-y-6'>
      <div>
        <h1 className='text-3xl font-bold text-[#09173c]'>
          Tableau de bord
        </h1>
        <p className='mt-2 text-slate-600'>
          Bienvenue dans votre espace étudiant. Consultez vos cours, vos notes et les informations importantes de votre profil.
        </p>
      </div>

      <div className='grid grid-cols-1 gap-5 md:grid-cols-2 xl:grid-cols-4'>
        <div className='rounded-2xl border border-slate-200 bg-white p-5 shadow-sm'>
          <p className='text-sm font-medium text-slate-500'>Mes cours</p>
          <h2 className='mt-3 text-3xl font-bold text-[#09173c]'>5</h2>
          <p className='mt-2 text-sm text-slate-500'>Cours inscrits ce semestre</p>
        </div>

        <div className='rounded-2xl border border-slate-200 bg-white p-5 shadow-sm'>
          <p className='text-sm font-medium text-slate-500'>Moyenne</p>
          <h2 className='mt-3 text-3xl font-bold text-[#09173c]'>84%</h2>
          <p className='mt-2 text-sm text-slate-500'>Résultat académique global</p>
        </div>

        <div className='rounded-2xl border border-slate-200 bg-white p-5 shadow-sm'>
          <p className='text-sm font-medium text-slate-500'>Évaluations</p>
          <h2 className='mt-3 text-3xl font-bold text-[#09173c]'>12</h2>
          <p className='mt-2 text-sm text-slate-500'>Évaluations planifiées</p>
        </div>

        <div className='rounded-2xl border border-slate-200 bg-white p-5 shadow-sm'>
          <p className='text-sm font-medium text-slate-500'>Crédits</p>
          <h2 className='mt-3 text-3xl font-bold text-[#09173c]'>15</h2>
          <p className='mt-2 text-sm text-slate-500'>Crédits en cours</p>
        </div>
      </div>

      <div className='grid grid-cols-1 gap-5 lg:grid-cols-2'>
        <div className='rounded-2xl border border-slate-200 bg-white p-6 shadow-sm'>
          <h3 className='text-xl font-bold text-[#09173c]'>
            Actions rapides
          </h3>

          <div className='mt-5 grid grid-cols-1 gap-4 sm:grid-cols-2'>
            <button
              onClick={() => navigate('/etudiant/mes-cours')}
              className='rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]'
            >
              Voir mes cours
            </button>

            <button
              onClick={() => navigate('/etudiant/mes-notes')}
              className='rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]'
            >
              Consulter mes notes
            </button>

            <button
              onClick={() => navigate('/etudiant/profil')}
              className='rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]'
            >
              Voir mon profil
            </button>

            <button
              onClick={() => navigate('/etudiant/mes-notes')}
              className='rounded-xl bg-[#0f255e] px-4 py-3 text-white font-semibold transition hover:bg-[#16337a]'
            >
              Voir les évaluations
            </button>
          </div>
        </div>

        <div className='rounded-2xl border border-slate-200 bg-white p-6 shadow-sm'>
          <h3 className='text-xl font-bold text-[#09173c]'>
            Aperçu académique
          </h3>

          <div className='mt-5 space-y-4 text-slate-600'>
            <div className='flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3'>
              <span>Programme</span>
              <span className='font-bold text-[#09173c]'>Informatique</span>
            </div>

            <div className='flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3'>
              <span>Session actuelle</span>
              <span className='font-bold text-[#09173c]'>Hiver 2026</span>
            </div>

            <div className='flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3'>
              <span>Statut</span>
              <span className='font-bold text-green-700'>Actif</span>
            </div>

            <div className='flex items-center justify-between rounded-xl bg-slate-50 px-4 py-3'>
              <span>Matricule</span>
              <span className='font-bold text-[#09173c]'>20260100</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  )
}