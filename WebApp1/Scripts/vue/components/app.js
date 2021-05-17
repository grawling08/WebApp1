var url = 'https://localhost:44326/Vue';

export default {
    name: 'App',
    data() {
        return {
            msg: "Welcome to Vue.js!",
            list: []
        };
    },
    template: `
    <h2>Vue</h2>
    <table class="table">
        <tr>
            <th>
                Avatar
            </th>
            <th>
                Username
            </th>
            <th>
                First Name
            </th>
            <th>
                Last Name
            </th>
            <th>
                Address
            </th>
            <th>
                Birthday
            </th>
            <th></th>
        </tr>
        <tr v-for="item in list">
            <td>
                <img v-if="item.pic === null" src="../Images/placeholder.jpg" /><img v-else :src="item.pic" width="80" height="80" />
            </td>
            <td>{{ item.username }}</td>
            <td>{{ item.fname }}</td>
            <td>{{ item.lname }}</td>
            <td>{{ item.address }}</td>
            <td>{{ item.dob }}</td>
            <td><a href="#">Open</a></td>
        </tr>
    </table>
  `,
    created: function () {
        axios.get(url + '/GetUsers')
            .then((response) => {
                //console.log(response.data);
                response.data.forEach((item, index) => {
                    response.data[index].pic = item.pic.replace("~", '../..');
                    //console.log(response.data);
                });
                this.list = response.data;
            })
            .catch((error) => {
                console.log(error);
            })
    }
};