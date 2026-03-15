import { Outlet, } from "react-router-dom"
import UDEM from "../../../assets/img/udem-auth.webp"

export const AuthLayout = () => {

  

  return (
    <section className="min-h-screen flex items-center justify-center bg-slate-100">

      <div className="w-225 h-130 bg-white shadow-2xl rounded-2xl overflow-hidden flex">

        {/* Image */}
        <div className="w-1/2 bg-[#09173c] flex items-center justify-center p-10">

          <img
            src={UDEM}
            alt="auth illustration"
            className="max-h-75 rounded-b-full "
          />

        </div>

        {/* Form */}
        <div className="w-1/2 flex items-center justify-center p-10">
          <Outlet />
        </div>

      </div>

    </section>
  )
}