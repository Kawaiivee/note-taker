<script setup lang="ts">
import { defineProps, onMounted } from 'vue';

const props = defineProps(['note', 'deleteClicked']);

onMounted(() => {
  console.log(props);
});

</script>

<template>
  <v-container>
    <v-card>
      <v-card-title>
        {{ `${props.note.title}: ${props.note.text}` }} 
      </v-card-title>
      <v-card-subtitle>
        {{ `${props.note.author.name}` }} 
      </v-card-subtitle>
      <v-card-text>
        {{ `${props.note.text}` }}
      </v-card-text>
      <v-card-action>
        <v-btn @click.prevent="props.deleteClicked(props.note.id)">
          <v-icon icon="mdi-delete"></v-icon>
        </v-btn>
      </v-card-action>
    </v-card>
  </v-container>
</template>
