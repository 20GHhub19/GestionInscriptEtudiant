import { NavLink, Outlet } from 'react-router-dom'

export const EtudiantLayout = () => {
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
          <h1 className='text-xl font-bold'>Portail Étudiant</h1>
          <p className='mt-1 text-sm text-slate-300'>Espace personnel</p>
        </div>

        <nav className='flex-1 space-y-2 px-4 py-6'>
          <NavLink to='/etudiant/dashboard' className={navLinkClass}>
            Tableau de bord
          </NavLink>

          <NavLink to='/etudiant/mes-cours' className={navLinkClass}>
            Mes cours
          </NavLink>

          <NavLink to='/etudiant/mes-notes' className={navLinkClass}>
            Mes notes
          </NavLink>

          <NavLink to='/etudiant/profil' className={navLinkClass}>
            Mon profil
          </NavLink>
        </nav>

        <div className='border-t border-white/10 px-4 py-4'>
          <NavLink
            to='/auth/login'
            className='flex w-full items-center justify-center rounded-lg bg-[#0f255e] px-4 py-3 text-sm font-semibold text-white transition hover:bg-[#16337a]'
          >
            Déconnexion
          </NavLink>
        </div>
      </aside>

      <div className='flex-1 flex flex-col'>
        <header className='bg-[#09173c] text-white px-6 py-4 flex items-center justify-between shadow'>
          <h2 className='text-2xl font-bold'>Espace Étudiant</h2>

          <div className='flex items-center gap-3 rounded-lg bg-[#0f255e] px-3 py-2'>
            <img
              src='https://i.pravatar.cc/40'
              alt='Étudiant'
              className='h-9 w-9 rounded-full border-2 border-white'
            />
            <span className='hidden md:block text-sm font-semibold'>
              Étudiant
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