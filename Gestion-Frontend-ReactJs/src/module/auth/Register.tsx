import { Link } from "react-router-dom"

export const Register = () => {
  return (
    <div className="w-full max-w-sm">

      <h2 className="text-2xl font-bold text-[#09173c] mb-6">
        Créer un compte
      </h2>

      <form className="flex flex-col gap-4">

        <input
          type="text"
          placeholder="Nom"
          className="border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]"
        />

        <input
          type="text"
          placeholder="Prénom"
          className="border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]"
        />

        <input
          type="email"
          placeholder="Courriel"
          className="border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]"
        />

        <input
          type="password"
          placeholder="Mot de passe"
          className="border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]"
        />

        <input
          type="password"
          placeholder="Confirmer le mot de passe"
          className="border border-gray-300 rounded-lg px-4 py-2 focus:outline-none focus:ring-2 focus:ring-[#0f255e]"
        />

        <button
          className="bg-[#0f255e] text-white py-2 rounded-lg hover:bg-[#16337a] transition font-semibold"
        >
          S'inscrire
        </button>

      </form>
<p className="text-center text-slate-600 mt-5">
        Vous avez déjà un compte ?{" "}
        <Link
          to="/auth/login"
          className="text-[#09173c] font-semibold hover:underline"
        >
          Se connecter
        </Link>
      </p>
    </div>
  )
}