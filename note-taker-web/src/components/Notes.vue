<script setup lang="ts">
import { getNotes } from '@/api/noteTakerApi';
import type { NoteModel } from '@/classes/types';
import { ref, onMounted } from 'vue';
import Note from '../components/Note.vue';

const notes: NoteModel[] = await getNotes();
</script>

<template>
    <v-container>
      <v-row v-for="note in notes">
        <Note
          :key="note.id"
          :note="note"
        />
      </v-row>
    </v-container>
</template>