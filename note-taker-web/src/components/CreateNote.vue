<script setup lang="ts">
import { ref } from 'vue';  //check local storage for a current default author...or use pinia
const note = ref({
  author: '',
  title: '',
  text: ''
});

const handleSaveButtonClicked = (event: Event): void => {
  event.preventDefault();
  console.log("hello world!");
  note.value.author = 'sheesh';
}
</script>

<template>
  <v-container>
    <v-row>
      <v-col>
        <v-text-field
        v-model="note.author"
        label="Note Author"
        required
        ></v-text-field>
        <v-text-field
        v-model="note.title"
        label="Note Title"
        variant="outlined"
        required
        ></v-text-field>
        <v-text-field
        v-model="note.text"
        label="Note Text"
        variant="outlined"
        required
        ></v-text-field>
      </v-col>
      <v-btn
        @click=handleSaveButtonClicked
      >Save Note</v-btn>
    </v-row>
  </v-container>
</template>
