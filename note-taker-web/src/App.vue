<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <v-app>
    <v-app-bar
    :elevation="2"
    >
      <RouterLink to="/create-note">
        <v-btn>Create Note</v-btn>
      </RouterLink>
      <RouterLink to="/notes">
        <v-btn>My Notes</v-btn>
      </RouterLink>
    </v-app-bar>
    <v-main>
      <v-container>
        <v-row>
          <v-col>
            <RouterView />
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>