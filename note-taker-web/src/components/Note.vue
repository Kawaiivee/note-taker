<script setup lang="ts">

import type { NoteModel } from '@/classes/types';
import { defineProps, onMounted, ref } from 'vue';

const props = defineProps(['note', 'deleteClicked']);
const note = props?.note;
const isEditing = ref<boolean>(false);
const editNote = ref<NoteModel>(note);

onMounted(() => {
  console.log(props);
});

const handleUpdateClicked = () => {
  const newNote = 
};

</script>

<template>
  <v-container>
    <v-card>
      <v-container v-if="!isEditing">
          <v-card-title>
            {{ `${note.title}` }} 
          </v-card-title>
          <v-card-subtitle>
            {{ `${note.author.name}` }} 
          </v-card-subtitle>
          <v-card-text>
            {{ `${note.text}` }}
          </v-card-text>
      </v-container>
      <v-container v-else>
        <v-text-field
          type="input"
          label="Note Title"
          v-model="editNote.title"
          variant="outlined"
          required
        />
        <v-textarea
          type="input"
          label="Note Text"
          v-model="editNote.text"
          variant="outlined"
          required
        />
        <v-text-field
          type="input"
          label="Author"
          v-model="editNote.author.name"
          required
        />
      </v-container>
      <v-card-action>
        <v-icon @click.prevent="() => isEditing = !isEditing" icon="mdi-pencil"></v-icon>
      </v-card-action>
      <v-card-action v-if="!isEditing">
        <v-icon @click.prevent="props.deleteClicked(props.note.id)" icon="mdi-delete"></v-icon>
      </v-card-action>
    </v-card>
  </v-container>
</template>
