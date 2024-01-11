<script setup lang="ts">
import { ref } from 'vue';  //check local storage for a current default author...or use pinia
import { addNote } from '../api/noteTakerApi';

const addNoteRequest = ref({
  authorName: '',
  noteTitle: '',
  noteText: ''
});

const handleSaveButtonClicked = async (event: Event) => {
  event.preventDefault();
  await addNote({
    authorName: addNoteRequest.value.authorName,
    noteTitle: addNoteRequest.value.noteTitle,
    noteText: addNoteRequest.value.noteText
  });
}
</script>

<template>
  <v-container>
    <v-row>
      <v-col>
        <v-text-field
          type="input"
          v-model="addNoteRequest.noteTitle"
          label="Note Title"
          variant="outlined"
          required
        />
        <v-textarea
          type="input"
          v-model="addNoteRequest.noteText"
          label="Note Text"
          variant="outlined"
          required
        />
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-text-field
          type="input"
          v-model="addNoteRequest.authorName"
          label="Note Author"
          required
        />
      </v-col>
      <v-col>
        <v-btn
          @click=handleSaveButtonClicked
        >
        Save Note
        </v-btn>
      </v-col>
    </v-row>
  </v-container>
</template>
