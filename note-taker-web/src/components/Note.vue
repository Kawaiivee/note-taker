<script setup lang="ts">

import { updateNote } from '@/api/noteTakerApi';
import type { NoteModel } from '@/classes/types';
import { watch } from 'vue';
import { ref } from 'vue';

const props = defineProps(['note', 'deleteClicked']);
const note = props?.note;
const isEditing = ref<boolean>(false);
const isNoteDirty = ref<boolean>(false);
const originalNote = ref<NoteModel>({
  id: note?.id ?? '',
  title: note?.title,
  text: note?.text,
  author: {
    id: note?.author?.id ?? '',
    name: note?.author?.name
  }
});
const editNote = ref<NoteModel>(note);

watch(() => editNote, (newNoteValue) => {
  if(
    newNoteValue.value.title !== originalNote.value.title ||
    newNoteValue.value.text !== originalNote.value.text
  ) {
    isNoteDirty.value = true;
  }
  else {
    isNoteDirty.value = false;
  }
}, { deep: true });   // checks for specific nested values too

const handleUpdateClicked = async () => {
  originalNote.value = JSON.parse(JSON.stringify(editNote.value));
  isNoteDirty.value = false;
  await updateNote(editNote.value);
  isEditing.value = false;
};

const handleCancelClicked = () => {
  note.value = JSON.parse(JSON.stringify(originalNote.value));
  editNote.value = JSON.parse(JSON.stringify(originalNote.value));
  isEditing.value = false;
};

</script>

<template>
  <v-container>
    <v-card>
      <v-container v-if="!isEditing">
          <v-card-title>
            {{ `${note?.title}` }} 
          </v-card-title>
          <v-card-subtitle>
            {{ `${note?.author?.name}` }} 
          </v-card-subtitle>
          <v-card-text>
            {{ `${note?.text}` }}
          </v-card-text>
      </v-container>
      <v-container v-else>
        <v-row>
          <v-text-field
            type="input"
            label="Note Title"
            v-model="editNote.title"
            variant="outlined"
            required
          />
        </v-row>
        <v-row>
          <v-textarea
            type="input"
            label="Note Text"
            v-model="editNote.text"
            variant="outlined"
            required
          />
        </v-row>
        <v-row>
          <v-col>
            Author: {{editNote.author.name}}
          </v-col>
        </v-row>
      </v-container>
      <v-container v-if="!isEditing">
        <v-card-action>
          <v-icon @click.prevent="() => isEditing = !isEditing" icon="mdi-pencil"></v-icon>
        </v-card-action>
        <v-card-action>
          <v-icon @click.prevent="props.deleteClicked(props?.note?.id)" icon="mdi-delete"></v-icon>
        </v-card-action>
      </v-container>
      <v-container v-else>
        <v-card-action>
          <v-icon
            @click.prevent="() => handleCancelClicked()"
            icon="mdi-close">
          </v-icon>
        </v-card-action>
        <v-card-action>
          <v-icon
            v-if="isNoteDirty"
            @click.prevent="() => handleUpdateClicked()"
            icon="mdi-check">
          </v-icon>
        </v-card-action>
      </v-container>
    </v-card>
  </v-container>
</template>
