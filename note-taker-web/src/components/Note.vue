<script setup lang="ts">
import { defineProps, onMounted } from 'vue';

const props = defineProps(['note']);

onMounted(() => {
  console.log(props);
});

const handleDeleteClicked = (event: MouseEvent) => {
  
};

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
    </v-card>
  </v-container>
</template>
