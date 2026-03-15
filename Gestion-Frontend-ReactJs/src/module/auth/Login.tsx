import { Link, useNavigate } from 'react-router-dom'
import { useState } from 'react'

export const Login = () => {
  const navigate = useNavigate()
  const [role, setRole] = useState('etudiant')

  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault()

    if (role === 'admin') {
      navigate('/admin/dashboard')
      return
    }

    navigate('/etudiant/dashboard')
  }

  return (
    <div className='w-full max-w-sm'>
        <h2 className='text-2xl font-bold text-[#09173c] mb-6'>Connexion</h2>
      <div className='flex  justify-between mb-5'>
        <label className='flex items-center gap-2 cursor-pointer'>
          <input
            type='radio'
            value='etudiant'
            checked={role === 'etudiant'}
            onChange={e => setRole(e.target.value)}
            className='accent-[#0f255e]'
          />
          Étudiant
        </label>

        <label className='flex items-center gap-2 cursor-pointer'>
          <input
            type='radio'
            value='admin'
            checked={role === 'admin'}
            onChange={e => setRole(e.target.value)}
            className='accent-[#0f255e]'
          />
          Administrateur
        </label>
      </div>

      <form className='flex flex-col gap-4' onSubmit={handleLogin}>
        <input
          type='email'
          placeholder='Email'
          className='border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]'
        />

        <input
          type='password'
          placeholder='Mot de passe'
          className='border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]'
        />

        <div className='flex gap-6 mt-2'></div>
        <button className='bg-[#0f255e] text-white py-2 rounded-lg hover:bg-[#16337a] transition'>
          Se connecter
        </button>
      </form>

      <p className='text-center text-sm text-gray-600 mt-6'>
        Vous n’avez pas de compte ?{' '}
        <Link
          to='/auth/register'
          className='text-[#0f255e] font-semibold hover:underline'
        >
          Créer un compte
        </Link>
      </p>
    </div>
  )
}
