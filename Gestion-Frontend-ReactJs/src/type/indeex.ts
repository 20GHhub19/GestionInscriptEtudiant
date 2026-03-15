export interface Programme {
  id: number
  code: string
  nom: string
  description: string
  credits: number
  unite: string
  duree: number
}
export interface Professeur {
  id: number
  nom: string
  prenom: string
  courriel: string
  grade: string
  statut: string
}
export interface Etudiant {
  id: number
  matricule: string
  nom: string
  prenom: string
  courriel: string
  statut: string
  programme: string
}
export interface Note {
  id: number
  etudiant: string
  evaluation: string
  cours: string
  noteNumerique: number
  noteLettre: string
}
export interface Inscription {
  id: number
  etudiant: string
  cours: string
  date: string
  statut: string
  tentative: number
}