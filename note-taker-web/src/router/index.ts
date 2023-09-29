import { createRouter, createWebHistory } from 'vue-router'
import CreateNoteView from '../views/CreateNoteView.vue';
import NotesView from '../views/NotesView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'notes',
      component: NotesView
    },
    {
      path: '/notes',
      name: '/notes',
      component: NotesView
    },
    {
      path: '/create-note',
      name: 'create note',
      component: CreateNoteView
    },
    // Lazy Load route example w/ code splitting -- generates CreateNoteView.[hash].js fhis route
    // {
    //   path: '/create-note',
    //   name: 'create note',
    //   component: () => import('../views/CreateNoteView.vue')
    // }
  ]
});

export default router;
