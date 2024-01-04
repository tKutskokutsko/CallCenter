<template>
    <div class="post">
        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <SortedTable :values="values">
                <thead>
                    <tr>
                        <th scope="col" style="text-align: left; width: 10rem;">
                            <SortLink name="id">AgentId</SortLink>
                        </th>
                        <th scope="col" style="text-align: left; width: 10rem;">
                            <SortLink name="name">AgentName</SortLink>
                        </th>
                        <th scope="col" style="text-align: left; width: 10rem;">
                            <SortLink name="hits">TimestampUtc</SortLink>
                        </th>
                        <th scope="col" style="text-align: left; width: 10rem;">
                            <SortLink name="hits">Action</SortLink>
                        </th>
                        <th scope="col" style="text-align: left; width: 10rem;">
                            <SortLink name="hits">GueueIds</SortLink>
                        </th>
                        <th scope="col" style="text-align: left; width: 10rem;">
                            <SortLink name="hits">AgentState</SortLink>
                        </th>
                    </tr>
                </thead>
                <tbody slot="body" slot-scope="sort">
                    <tr v-for="forecast in post" :key="forecast.date">
                        <td>{{ forecast.agentId }}</td>
                        <td>{{ forecast.agentName }}</td>
                        <td>{{ forecast.timestampUtc }}</td>
                        <td>{{ forecast.action }}</td>
                        <td>{{ forecast.queueIds }}</td>
                        <td>{{ forecast.agentState }}</td>
                    </tr>
                </tbody>
            </SortedTable>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

                fetch('weatherforecast')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            }
        },
    });
</script>














