import { NavLink, Outlet } from 'react-router-dom'

export const AdminLayout = () => {
  const navLinkClass = ({ isActive }: { isActive: boolean }) =>
    `block rounded-lg px-4 py-3 font-medium transition ${
      isActive
        ? 'bg-[#0f255e] text-white'
        : 'text-slate-200 hover:bg-[#16337a] hover:text-white'
    }`

  return (
    <section className='min-h-screen bg-slate-100 flex'>
      <aside className='w-72 bg-[#09173c] text-white flex flex-col'>
        <div className='border-b border-white/10 px-6 py-5'>
          <h1 className='text-xl font-bold'>Gestion Inscript Étudiant</h1>
          <p className='mt-1 text-sm text-slate-300'>Espace administrateur</p>
        </div>

        <nav className='flex-1 space-y-2 px-4 py-6'>
          <NavLink to='/admin/dashboard' className={navLinkClass}>
            Tableau de bord
          </NavLink>

          <NavLink to='/admin/programmes' className={navLinkClass}>
            Programmes
          </NavLink>

          <NavLink to='/admin/etudiants' className={navLinkClass}>
            Étudiants
          </NavLink>

          <NavLink to='/admin/cours' className={navLinkClass}>
            Cours
          </NavLink>

          <NavLink to='/admin/professeurs' className={navLinkClass}>
            Professeurs
          </NavLink>

          <NavLink to='/admin/inscriptions' className={navLinkClass}>
            Inscriptions
          </NavLink>

          <NavLink to='/admin/notes' className={navLinkClass}>
            Notes
          </NavLink>
        </nav>

        <div className='border-t border-white/10 px-4 py-4'>
          <NavLink
            to='/auth/login'
            className='block w-full text-center rounded-lg bg-[#0f255e] px-4 py-3 text-sm font-semibold text-white transition hover:bg-[#16337a]'
          >
            Déconnexion
          </NavLink>
        </div>
      </aside>

      <div className='flex-1 flex flex-col'>
        <header className='bg-[#09173c] text-white px-6 py-4 flex items-center justify-between shadow'>
          <h2 className='text-2xl font-bold'>Administration</h2>

          <div className='flex items-center gap-3 rounded-lg bg-[#0f255e] px-3 py-2'>
            <img
              src='https://i.pravatar.cc/40'
              alt='Administrateur'
              className='h-9 w-9 rounded-full border-2 border-white'
            />
            <span className='hidden md:block text-sm font-semibold'>
              Administrateur
            </span>
          </div>
        </header>

        <main className='flex-1 p-6'>
          <div className='min-h-full rounded-2xl bg-white p-6 shadow-sm'>
            <Outlet />
          </div>
        </main>
      </div>
    </section>
  )
}
