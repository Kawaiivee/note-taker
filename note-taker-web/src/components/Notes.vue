<script setup lang="ts">

import { getNotes, deleteNote } from '@/api/noteTakerApi';
import type { NoteModel } from '@/classes/types';
import { ref, onMounted } from 'vue';
import Note from '../components/Note.vue';

const notes = ref<NoteModel[]>([]);

onMounted(async () => {
  notes.value = await getNotes();
});

const handleDeleteClicked = async (id: string) => {
  await deleteNote(id);
  notes.value = await getNotes();
  console.log(Date.now());
};

const handleLocalDeleteClicked = async (id: string) => {
  console.log(Date.now());
}

</script>

<template>
  <v-container>
    <v-row v-for="note in notes">
      <Note
        :key="note.id"
        :note="note"
        :deleteClicked="handleDeleteClicked"
        :localDeleteClicked="handleLocalDeleteClicked"
      />
    </v-row>
  </v-container>
</template>