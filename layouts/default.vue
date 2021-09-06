<template>
  <v-app>
    <v-navigation-drawer
      v-model="drawer"
      :mini-variant="miniVariant"
      :clipped="clipped"
      fixed
      app
    >
      <div v-if='$auth.loggedIn'>
        <tree-menu :label="tree.label"
                   :nodes="tree.nodes"
                   :icon='tree.icon'
                   :to='tree.to'
                   :depth='0'>
        </tree-menu>
      </div>
    </v-navigation-drawer>
    <v-app-bar
      :clipped-left="clipped"
      fixed
      app
    >
      <v-app-bar-nav-icon @click.stop="drawer = !drawer" />
      <v-btn
        icon
        @click.stop="miniVariant = !miniVariant"
      >
        <v-icon>mdi-{{ `chevron-${miniVariant ? 'right' : 'left'}` }}</v-icon>
      </v-btn>
      <v-btn
        icon
        @click.stop="clipped = !clipped"
      >
        <v-icon>mdi-application</v-icon>
      </v-btn>
      <v-btn
        icon
        @click.stop="fixed = !fixed"
      >
        <v-icon>mdi-minus</v-icon>
      </v-btn>
      <v-toolbar-title v-text="title" />
      <v-spacer />
      <div v-if='$auth.loggedIn'>
        <v-btn text @click='$auth.logout()' to = "/logoutPage"> Logout </v-btn>
      </div>
      <div v-else>
        <v-btn text to = "/login"> Login </v-btn>
        <v-btn text to = "/register"> Register </v-btn>
      </div>
      <v-btn
        icon
        @click.stop="rightDrawer = !rightDrawer"
      >
        <v-icon>mdi-menu</v-icon>
      </v-btn>
    </v-app-bar>
    <v-main>
      <v-container>
        <Nuxt />
      </v-container>
    </v-main>
    <v-navigation-drawer
      v-model="rightDrawer"
      :right="right"
      temporary
      fixed
    >
      <v-list>
        <v-list-item @click.native="right = !right">
          <v-list-item-action>
            <v-icon light>
              mdi-repeat
            </v-icon>
          </v-list-item-action>
          <v-list-item-title>Switch drawer (click me)</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-footer
      :absolute="!fixed"
      app
    >
      <span>&copy; {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
import TreeMenu from '../components/TreeMenu'
export default {
  components : {
    TreeMenu
  },
  data () {
    return {
      clipped: false,
      drawer: false,
      fixed: false,
      display : false,
      tree : {
        label : "menu",
        nodes : [
          {
            icon: 'mdi-apps',
            label: 'Welcome',
            to: '/'
          },
          {
            label : 'Books',
            icon : 'mdi-duck',
            nodes : [
              {
                label : 'Book List',
                to : '/Books'
              },
              {
                label : 'Favourite Books',
                to : '/FavouriteBooks'
              }
            ],
          },
          {
            label : 'Authors',
            icon : 'mdi-duck',
            nodes : [
              {
                label: 'Author List',
                to : '/Authors'
              },
              {
                label: 'Favourite Authors',
                to : '/favouriteAuthors'
              }
            ],
          }
        ],
      },
      miniVariant: false,
      right: true,
      rightDrawer: false,
      title: 'BookStore'
    }
  },
  methods : {
    displaySubs() {
      this.display = !this.display;
    }
  }
}
/* Use @click and with a function write the sub info */
</script>
<style>
.sub {
  outline: 0;
  box-shadow: none;
  border: none;
  color: blue;
}
</style>
