<script setup lang="ts">
import { onMounted } from 'vue';

// import type { NoteModel } from '@/classes/types';

// export interface NoteProps {
//   note: NoteModel;
// };

const props = defineProps();

onMounted(() => {
  console.log(props);
});
</script>

<template>
  <v-container>
    <v-card> ton
      <!-- <v-card-title>
        {{ `${props.note.title}: ${props.note.text}` }} 
      </v-card-title>
      <v-card-subtitle>
        {{ `${props.note.author.name}` }} 
      </v-card-subtitle>
      <v-card-text>
        {{ `${props.note.text}` }}
      </v-card-text> -->
    </v-card>
  </v-container>
</template>
