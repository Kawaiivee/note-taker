<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import CreateNoteView from './views/CreateNoteView.vue'
</script>

<template>
  <v-app>
    <header>
      <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="125" height="125" />

      <div class="wrapper">
        <HelloWorld msg="You did it!" />

        <nav>
          <RouterLink to="/create-note">Create Note</RouterLink>
          <RouterLink to="/notes">Notes</RouterLink>
        </nav>
      </div>
    </header>

    <RouterView />
  </v-app>
</template>